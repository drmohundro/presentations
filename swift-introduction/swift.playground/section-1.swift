// Playground - noun: a place where people can play

import Cocoa

// ------- variables --------

// implicit typing
var x = "Hello World!"
var y = 1

// explicit typing
var explicitString: String = "hello world"
var explicitInt: Int = 1

// immutable variables (think constants, except more powerful)
let immutable = "I can't change after this"
let pi = 3.14159265359


















// ------- collections --------
let arr = [1,2,3]
let strArr = ["hello", "world", "!"]
arr[0]
strArr[1]

let hash = [1:"a", 2:"b", 3:"c"]
let strHash = ["firstName":"David", "lastName":"Mohundro"]
hash[1]
strHash["lastName"]


















// ------- control flow --------
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



















// ------- functions --------
func noParams() {
    "Hello"
}
noParams()


func sayHello(name:String) {
    "Hello \(name)"
}
sayHello("Mo")


func getFirstAndLastName(firstName:String, lastName:String) -> String {
    return "Your name is \(firstName) \(lastName)"
}
getFirstAndLastName("David", "Mohundro")



















// ------- classes --------
class Person {
    var firstName: String
    var lastName: String
    var age: Int
    
    var fullName: String {
        get {
            return "\(self.firstName) \(self.lastName)"
        }
    }
    
    init(firstName:String, lastName:String, age:Int) {
        self.firstName = firstName
        self.lastName = lastName
        self.age = age
    }
    
    func eat(food:String, withDrink:String) {
        "\(self.fullName) is about to eat \(food) and enjoy \(withDrink) to drink"
    }
}

// type this out... let them see how intellisense behaves differently
// it's an Obj-C thing
var person = Person(firstName: "David", lastName: "Mohundro", age: 33)
person.fullName
// again, notice the intellisense (first parameter isn't required... second one is)
person.eat("Pizza", withDrink: "Coke")




















// ------- protocols --------
protocol CoolCat {
    func Greetings() -> String
}

class CoolChecker {
    func IsCool(cat: CoolCat) -> Bool {
        cat.Greetings()
        return true
    }
    
    func IsCool(cat: Any) -> Bool {
        "sad trombone"
        return false
    }
}

class TheFonz : CoolCat {
    func Greetings() -> String {
        return "Heyyyy"
    }
}

class Bubba {
}

var checker = CoolChecker()
checker.IsCool(TheFonz())
checker.IsCool(Bubba())




















// ------- enums --------
enum AwesomenessLevel {
    case Lame, Normal, Awesome
}

let level = AwesomenessLevel.Awesome

enum IntAwesomenessLevel: Int {
    case Lame = 0           // initial value is optional
    case Normal
    case Awesome
}
let intLevel = IntAwesomenessLevel.Awesome
intLevel.rawValue // provided because of inheriting from Int

enum StringEnumExample: String {
    case None = "None"
    case All = "All"
    //case Some               // Will result in error: "Enum case must declare a raw value when the preceding raw value is not an integer"
}
let strEnum = StringEnumExample.None
StringEnumExample(rawValue: "All") == StringEnumExample.All




















// ------- extensions --------
extension Int {
    func Double() -> Int {
        return self * 2
    }
    
    mutating func DoubleSelf() {
        self = self * 2
    }
}
2.Double()
var extensionExample = 4
extensionExample.DoubleSelf()




















// ------- generics --------
var genericArray = Array<Int>()
genericArray.append(5)




















// ------- functional aspecs --------
// really wordy... ("in" is part of the parameter statement)
[1,2,3].filter({
    (x: Int) -> Bool in
    return x % 2 == 0
})

// exclude the return type (implicit!) and return keyword!
[1,2,3].filter({
    x in
    x % 2 == 0
})

// or even omit the parameters completely and use $0, $1, etc.
[1,2,3].filter({
    $0 % 2 == 0
})

// or finally, exclude the parens if it is the final statement (feels like Ruby blocks now)
let results = ["abc", "def", "ghi"]
    .filter { $0.hasPrefix("d") }
    .map { $0.uppercaseString }
results



