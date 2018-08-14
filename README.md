# Edge Redirector

In Windows 10, Cortana search result and some help links are forced to use Microsoft Edge to open. This app will associate `microsoft-edge` protocol that you can use system default browser or any browser to open the links. In addition, it can redirect Bing search to other search engines without browser extension required.

This app optimized for minimum startup time by using AOT compiler (CoreRT) to generate native code.

## Screenshots

<img alt="Screenshot of settings" src="assets\screenshot-settings.png" width="513">

## Build

Prerequisites:

- Windows 10 64-Bit Anniversary Update (Build 14393) or higher
- Visual Studio 2017 version 15.5 or higher with .NET desktop development and Universal Windows Platform development workload
- Windows 10 SDK (10.0.17134)
- .NET Core 2.1 SDK

Build and deploy `src\EdgeRedirector.Package`.

## License

This project is licensed under the [MIT License](LICENSE)

Copyright (c) 2018 [Brandon Hill](https://branhill.com/)
