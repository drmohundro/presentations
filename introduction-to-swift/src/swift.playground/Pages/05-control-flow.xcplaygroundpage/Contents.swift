/*: [Previous](@previous)
 
 ## Control Flow
 
 */

for item in ["abc", "def", "ghi"] {
    if item.hasPrefix("d") {
        "Matched on \(item)"
    }
}

let vegetable = "red pepper"
switch vegetable {
case "celery":
    let vegetableComment = "Add some raisins and make ants on a log."
case "cucumber", "watercress":
    let vegetableComment = "That would make a good tea sandwich."
case let x where x.hasSuffix("pepper"):
    let vegetableComment = "Is it a spicy \(x)?"
default:
    let vegetableComment = "Everything tastes good in soup."
}

//: [Next](@next)
