/*: [Previous](@previous)
 
 ## Protocols
 
 */
protocol CoolCat {
    func Greetings() -> String
}

class CoolChecker {
    func IsCool(cat: CoolCat) -> Bool {
        cat.Greetings()
        return true
    }
    
    func IsCool(cat: Any) -> Bool {
        print("sad trombone")
        return false
    }
}

class TheFonz : CoolCat {
    func Greetings() -> String {
        return "Ayyy!!!"
    }
}

class Bubba {
}

var checker = CoolChecker()
checker.IsCool(cat: TheFonz())
checker.IsCool(cat: Bubba())

//: [Next](@next)
