# Using Swift

<!-- .slide: data-background-image="https://user-images.githubusercontent.com/43072/39097677-1871c2f4-4625-11e8-9d07-f163d21c3c17.gif" -->

<!-- .slide: class="shadowed-text" -->

<..>

## What Do You Want To Build?

![BBQ](https://user-images.githubusercontent.com/43072/39097632-a4712a52-4624-11e8-97b1-cbd7c2c8bd49.gif)

<..>

## iOS App <!-- .element: style="text-transform: none" -->

Just use the wizard in XCode!

<..>

## Dependencies

* Carthage
* CocoaPods
* Swift Package Manager (SPM)

<..>

## CocoaPods

```bash
pod init
```

```ruby
# Podfile
platform :ios, '10.0'
use_frameworks!

target 'YOUR_TARGET_NAME' do
  pod 'SOME_POD', '~> 3.4.0'
end
```

```bash
pod install
```

<..>

## Carthage

```
# Cartfile
github 'SOMEORG/SOMEREPO' ~> 3.4.0
```

<..>

## Using Packages

```swift
// ...
    dependencies: [
        .package(
          url: "https://github.com/Alamofire/Alamofire.git",
          from: "4.0.0")
    ],
    targets: [
        .target(
            name: "MyFirstProgram",
            dependencies: ["Alamofire"]),
    ]
// ...
```

```bash
swift package update
```

<..>

## Command Line App

```bash
mkdir MyFirstProgram
cd MyFirstProgram
swift package init --type executable
swift run
```

(see [Usage.md](https://github.com/apple/swift-package-manager/blob/master/Documentation/Usage.md))

<..>

## Package

```bash
mkdir MyFirstPackage
cd MyFirstPackage
swift package init
swift test
```

<..>

## What if I want to use Xcode?

```bash
swift package generate-xcodeproj
```

(yes... you could just start in Xcode, too...)

<..>

# Linux?

<!-- .slide: data-background-image="https://user-images.githubusercontent.com/43072/39091332-9fcd7932-45b7-11e8-8619-a0e927ce53d1.gif" -->

<!-- .slide: class="shadowed-text" -->

<..>

## Using on Linux

```bash
# via swift.org
wget https://swift.org/builds/swift-4.0.3-release/ubuntu1610/swift-4.0.3-RELEASE/swift-4.0.3-RELEASE-ubuntu16.10.tar.gz
tar xzf swift-4.0.3-RELEASE-ubuntu16.10.tar.gz
./swift-4.0.3-RELEASE-ubuntu16.10.tar.gz/usr/bin/swift --version
```

<..>

## What about web apps?

![Yep](https://user-images.githubusercontent.com/43072/39097663-f03bd8a6-4624-11e8-851e-8c010281b9bd.gif)

<..>

## Options

* [Vapor](https://vapor.codes/)
* [Kitura](http://www.kitura.io/)

<..>

## Machine Learning!

[Swift on TensorFlow](https://www.tensorflow.org/community/swift)

> [It] is a result of first-principles thinking applied to machine learning
> frameworks, and works quite differently than existing TensorFlow language
> bindings ... [it] is based on the belief that machine learning is important
> enough to deserve first-class language and compiler support.

Note:
Coming in April 2018!
