properties {
}

Task default -depends Compile

Task Compile -depends Test {
    # note that msbuild is already in your PATH
    msbuild /version
}

Task Test {
    "testing!"
}
