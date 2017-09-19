/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        clean: ["wwwroot/lib/*", "temp/"],
        concat: {
            all: {
                src: ['wwwroot/js/test1.js', 'wwwroot/js/test2.js'],
                dest: 'wwwroot/temp/combined.js'
            }
        },
        jshint: {
            files: ['wwwroot/temp/*.js'],
            options: {
                '-W069': false,
            }
        },
        uglify: {
            all: {
                src: ['wwwroot/temp/combined.js'],
                dest: 'wwwroot/lib/combined.min.js'
            }
        },
        watch: {
            files: ["wwwroot/lib/*.js"],
            tasks: ["all"]
        }
    });

    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.registerTask("all", ['clean', 'concat', 'jshint', 'uglify']);
};