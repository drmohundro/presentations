/*: [Previous](@previous)
 
 ## Extensions
 
 */

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
extensionExample

let foo = 5
foo.DoubleSelf()

foo.Double()

//: [Next](@next)
