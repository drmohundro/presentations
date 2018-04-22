/*: [Previous](@previous)
 
 ## Object Oriented Programming
 
 */

class Person {
    var firstName: String
    var lastName: String
    var age: Int
    
    var fullName: String {
        get {
            return "\(self.firstName) \(self.lastName)"
        }
    }
    
    init(firstName: String, lastName: String, age:Int) {
        self.firstName = firstName
        self.lastName = lastName
        self.age = age
    }
    
    func eat(food: String, withDrink: String) {
        "\(self.fullName) is about to eat \(food) and enjoy \(withDrink) to drink"
    }
}

var person = Person(firstName: "David", lastName: "Mohundro", age: 29)
person.fullName


person.eat(food: "Sandwich", withDrink: "Diet Dr. Pepper")


person.eat(food: "Pizza", withDrink: "Coke")

//: [Next](@next)
