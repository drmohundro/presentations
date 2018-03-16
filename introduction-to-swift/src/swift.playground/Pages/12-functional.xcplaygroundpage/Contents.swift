/*: [Previous](@previous)
 
 ## Functional Programming
 
 */

// really wordy... ("in" is part of the parameter statement)
[1,2,3].filter({
    (x: Int) -> Bool in
    return x % 2 == 0
})

// exclude the return type (implicit!) and return keyword!
[1,2,3].filter({ x in x % 2 == 0
})

// or even omit the parameters completely and use $0, $1, etc.
[1,2,3].filter({
    $0 % 2 == 0
})

// or finally, exclude the parens if it is the final statement (feels like Ruby blocks now)
let results = ["abc", "def", "ghi"]
    .filter { $0.hasPrefix("d") }
    .map { $0.uppercased() }
results

//: [Next](@next)
