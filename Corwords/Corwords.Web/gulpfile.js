/// <binding BeforeBuild='clean' AfterBuild='default' Clean='clean' />
"use strict";

// Requires
var gulp = require("gulp"),
    rimraf = require("rimraf"),
    fs = require("fs"),
    path = require("path"),
    copy = require("gulp-copy"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    rename = require("gulp-rename"),
    sass = require("gulp-sass"),
    sourcemaps = require("gulp-sourcemaps"),
    uglify = require("gulp-uglify");


// Path array
var paths = {
    webroot: "./wwwroot/"
};

paths.assets = "./.assets/";
paths.corwords = "./.corwords/";

paths.js = paths.webroot + "js/";
paths.css = paths.webroot + "css/";

paths.jsSite = paths.js + "corwords.js";
paths.cssSite = paths.css + "corwords.css";

paths.npmBootstrap = paths.assets + "bootstrap-sass/assets/";
paths.npmBootswatch = paths.assets + "bootswatch/";


// Functions
function getFolders(dir) {
    return fs.readdirSync(dir)
        .filter(function (file) {
            return fs.statSync(path.join(dir, file)).isDirectory();
        });
}


// Clean the CSS and JS folders
gulp.task("clean:css", function (cb) {
    rimraf(paths.cssSite, cb);
});

gulp.task("clean:js", function (cb) {
    rimraf(paths.jsSite, cb);
});


// Copy files
gulp.task("copy:bootstrap", function () {
    return gulp.src([paths.npmBootstrap + 'javascripts/bootstrap.js', paths.npmBootstrap + 'javascripts/bootstrap.min.js'])
        .pipe(copy(paths.js, { prefix: 4 }));
});

gulp.task("copy:corwords", function () {
    return gulp.src([paths.corwords + 'scripts/*.js'])
        .pipe(concat(paths.jsSite))
        .pipe(gulp.dest('.'));
});

gulp.task("copy:fontawesome", function () {
    return gulp.src([paths.assets + 'fontawesome/fonts/*', paths.assets + 'fontawesome/css/*'])
        .pipe(copy(paths.webroot, { prefix: 2 }));
});

gulp.task("copy:jquery", function () {
    return gulp.src([paths.assets + 'jquery/dist/jquery.js', paths.assets + 'jquery/dist/jquery.min.js'])
        .pipe(copy(paths.js, { prefix: 3 }));
});


// Compile the SASS
gulp.task('sass:corwords:dev', function () {
    return gulp.src(paths.corwords + "style/corwords.scss")
        .pipe(sourcemaps.init())
        .pipe(sass().on('error', sass.logError))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(paths.webroot + 'css'));
});

gulp.task('sass:corwords:prod', function () {
    return gulp.src(paths.corwords + "style/corwords.scss")
        .pipe(sass({ outputStyle: 'compressed' }).on('error', sass.logError))
        .pipe(rename({suffix: '.min'}))
        .pipe(gulp.dest(paths.webroot + 'css'));
});

gulp.task("sass:corwords", ["sass:corwords:dev", "sass:corwords:prod"]);

gulp.task('sass:bootstrap:dev', function () {
    return gulp.src(paths.corwords + "style/custom_bootstrap.scss")
        .pipe(sourcemaps.init())
        .pipe(sass({ includePaths: paths.npmBootstrap + 'stylesheets' }).on('error', sass.logError))
        .pipe(sourcemaps.write())
        .pipe(rename({ basename: 'bootstrap' }))
        .pipe(gulp.dest(paths.webroot + 'css'));
});

gulp.task('sass:bootstrap:prod', function () {
    return gulp.src(paths.corwords + "style/custom_bootstrap.scss")
        .pipe(sass({ includePaths: paths.npmBootstrap + 'stylesheets', outputStyle: 'compressed' }).on('error', sass.logError))
        .pipe(rename({ basename: 'bootstrap', suffix: '.min' }))
        .pipe(gulp.dest(paths.webroot + 'css'));
});

gulp.task("sass:bootstrap", ["sass:bootstrap:dev", "sass:bootstrap:prod"]);

gulp.task('sass:bootswatch:prod', function () {
    var bootswatchFiles = getFolders(paths.npmBootswatch);

    var tasks = bootswatchFiles.map(function (folder) {
        if (folder == "fonts")
            return;

        return gulp.src(paths.corwords + "style/custom_bootswatch.scss")
            .pipe(sass({ includePaths: [paths.npmBootstrap + 'stylesheets', paths.npmBootswatch + folder], outputStyle: 'compressed' }).on('error', sass.logError))
            .pipe(rename({ basename: 'bootswatch.' + folder, suffix: '.min' }))
            .pipe(gulp.dest(paths.webroot + 'css'));
    });

    return tasks;
});

gulp.task('sass:bootswatch:dev', function () {
    var bootswatchFiles = getFolders(paths.npmBootswatch);

    var tasks = bootswatchFiles.map(function (folder) {
        if (folder == "fonts")
            return;

        return gulp.src(paths.corwords + "style/custom_bootswatch.scss")
            .pipe(sourcemaps.init())
            .pipe(sass({ includePaths: [paths.npmBootstrap + 'stylesheets', paths.npmBootswatch + folder] }).on('error', sass.logError))
            .pipe(rename({ basename: 'bootswatch.' + folder }))
            .pipe(sourcemaps.write())
            .pipe(gulp.dest(paths.webroot + 'css'));
    });

    return tasks;
});

gulp.task("sass:bootswatch", ["sass:bootswatch:dev", "sass:bootswatch:prod"]);


// Build the CSS and JS files
gulp.task("min:corwords:js", function () {
    return gulp.src(paths.jsSite)
        .pipe(uglify())
        .pipe(rename({ basename: 'corwords', suffix: '.min' }))
        .pipe(gulp.dest(paths.js));
});


// Define Roll-up Tasks
gulp.task("clean", ["clean:js", "clean:css"]);
gulp.task("copy", ["copy:jquery", "copy:bootstrap", "copy:fontawesome"]);
gulp.task("min", ["min:corwords:js"]);
gulp.task("sass", ["sass:corwords", "sass:bootstrap", "sass:bootswatch"]);

gulp.task("default", ["copy", "min", "sass"]);