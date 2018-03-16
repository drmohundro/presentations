/*: [Previous](@previous)
 
 ## Enums
 
 */

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


enum Foo {
    case IsString
    case IsNotString
    
    init(any: Any) {
        if any is String {
            self = .IsString
        }
        self = .IsNotString
    }
}

let f = Foo(any: "String")
switch f {
case .IsString:
    print("HI")

default:
    print("UH OH")

}

let 
//: [Next](@next)
