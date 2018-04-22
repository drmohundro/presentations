import XCTest
@testable import MyFirstPackage

class WhatIsTodayTests: XCTestCase {
    func testWhatIsToday() {
        // This is an example of a functional test case.
        // Use XCTAssert and related functions to verify your tests produce the correct
        // results.
        XCTAssertTrue(WhatIsToday().today() == Date())
    }

    // static var allTests = [
    //     ("testWhatIsToday", testWhatIsToday),
    // ]
}
