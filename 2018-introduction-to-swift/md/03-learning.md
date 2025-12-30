# Basics

<!-- .slide: data-background-image="https://user-images.githubusercontent.com/43072/39097647-cdc1a256-4624-11e8-9c05-ed8775a8cd13.gif" -->

<!-- .slide: class="shadowed-text" -->

<..>

## Variables

```swift
// string type, explicitly defined
var x: String = "Hello World"

// integer type, implicitly defined
var y = 5

// immutable!
let pi = 3.14159265359
```

Note:
Don't be confused, this _isn't_ dynamic typing like in JavaScript; this is more akin to Haskell in that the compiler determines the type at compile time.

Also, immutable types **cannot** change. Show this off.

<..>

## Collections

```swift
var arr = [1, 2, 3]

let hash = [1: "a", 2: "b", 3: "c"]
```

Note:
Notice the methods that exist on the mutable array versus the immutable array. Same thing with the hash. The compiler will not let you mess up.

<..>

## Control Flow

```swift
for item in ["abc", "def", "ghi"] {
    if item.hasPrefix("d") {
        print("Matched on \(item)")
    }
}
```

Note:
The other normal control flow statements exist, too, like while loops.

<..>

## Functions

```swift
func getFirstAndLastName(firstName: String, lastName: String) -> String {
    return "Your name is \(firstName) \(lastName)"
}
getFirstAndLastName(firstName: "David", lastName: "Mohundro")

func noParameterName(_ parm: String) -> String {
    return "Your parm is \(parm)";
}
noParameterName("I didn't have to specify the parameter name!")
```

Note:
Strangely enough, the function format reminds me of VB more than anything (with the return type at the end)

<-->

# Advanced Features

<!-- .slide: data-background-image="https://user-images.githubusercontent.com/43072/39097631-a462284a-4624-11e8-8901-fc058e4fc0d9.gif" -->

<!-- .slide: class="shadowed-text" -->

<..>

## Classes

```swift
class Person {
    var firstName: String
    var lastName: String
    var age: Int

    var fullName: String {
        return "\(self.firstName) \(self.lastName)"
    }

    private var myFavoriteFood: String?

    public var favoriteFood: String {
        get {
            if let fav = myFavoriteFood {
                return fav
            }
            return "Nothing :sad:"
        }
        set(newFavoriteFood) {
            myFavoriteFood = newFavoriteFood
        }
    }

    init(firstName: String, lastName: String, age: Int) {
        self.firstName = firstName
        self.lastName = lastName
        self.age = age
    }

    func eat(food: String, withDrink: String) {
        print("\(self.fullName) is about to eat \(food) and enjoy \(withDrink) to drink")
    }
}
```

<..>

## Protocols

```swift
protocol CoolCat {
    func greetings() -> String
}
```

<..>

## Enums

```swift
enum AwesomenessLevel {
    case lame, normal, awesome
}

let level = AwesomenessLevel.awesome
```

<..>

## Extensions

```swift
extension Int {
    func double() -> Int {
        return self * 2
    }

    mutating func doubleSelf() {
        self *= 2
    }
}
```

<..>

## Generics

```swift
// shorthand for Array<Int>()
var genericArray = [Int]()
genericArray.append(5)
```

<..>

## Functional

```swift
let results = ["abc", "def", "ghi"]
    .filter { $0.hasPrefix("d") }
    .map { $0.uppercased() }
```

Note:
Really begins to feel like a next-gen language with this.
