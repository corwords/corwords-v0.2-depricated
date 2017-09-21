/// <binding BeforeBuild='clean' AfterBuild='default' Clean='clean' />
"use strict";

// Requires
var gulp = require("gulp"),
    rimraf = require("rimraf"),
    copy = require("gulp-copy"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

// Path array
var paths = {
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/";
paths.css = paths.webroot + "css/";

paths.jsExpandedFiles = paths.js + "**/*.js";
paths.cssExpandedFiles = paths.css + "**/*.css";

paths.jsMinFiles = paths.js + "**/*.min.js";
paths.cssMinFiles = paths.css + "**/*.min.css";

paths.jsSite = paths.js + "corwords.min.js";
paths.cssSite = paths.css + "corwords.min.css";

paths.npmBootstrap = "./node_modules/bootstrap-sass/assets/";


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
        .pipe(copy(paths.js, { prefix: 4 }));;
});

gulp.task("copy:fontawesome", function () {
    return gulp.src(['./.assets/font-awesome-4.7.0/fonts/*', './.assets/font-awesome-4.7.0/css/*'])
        .pipe(copy(paths.webroot, { prefix: 2 }));;
});


// Build the CSS and JS files
gulp.task("min:js", function () {
    return gulp.src([paths.jsExpandedFiles, "!" + paths.jsMinFiles], { base: "." })
        .pipe(concat(paths.jsSite))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.cssExpandedFiles, "!" + paths.cssMinFiles], { base: "." })
        .pipe(concat(paths.cssSite))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});


// Define Roll-up Tasks
gulp.task("clean", ["clean:js", "clean:css"]);
gulp.task("copy", ["copy:bootstrap", "copy:fontawesome"]);
gulp.task("min", ["min:js", "min:css"]);
gulp.task("default", ["copy", "min"]);