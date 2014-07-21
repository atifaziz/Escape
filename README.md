# Escape

Escape is a JavaScript parser for .NET applications based on the [Esprima](http://esprima.org/) code base. In the same spirit, it serves as a building block for tools like compilers, [interpreters](https://github.com/sebastienros/jint), code instrumentation, editor auto-completion and the like.

## Usage

Escape is [distributed as a NuGet package](https://www.nuget.org/packages/Escape).
    
    const string js = @"
        // Life, Universe, and Everything
        var answer = 6 * 7;";
    var parser = new JavaScriptParser();
    var program = parser.Parse(js);

## History

The [Esprima](http://esprima.org/) [code base](https://github.com/ariya/esprima/blob/master/esprima.js) was ported to C# by [Sébastien Ros](http://about.me/sebastienros) as part of version 2.0 of the [Jint](https://github.com/sebastienros/jint) project, a Javascript interpreter for .NET. Escape is a fork to make the parser available as a stand-alone project/library so that it could be used, maintained and improved separately from Jint's interpreter and run-time and enable other tooling.

## Roadmap

Except for the namespace change, the goal for Escape 1.0 is to be *source-compatible* with [Jint 2.2](https://www.nuget.org/packages/Jint/2.2.0) release. Escape 2.0 will be a clean-up of the parser API.
