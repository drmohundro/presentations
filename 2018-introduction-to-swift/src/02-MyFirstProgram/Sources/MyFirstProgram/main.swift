print(Foo().sayHi())

import Foundation
import Alamofire

let response = Alamofire.request("http://www.google.com").responseString { response in
  if let val = response.result.value {
    print(val)
  }
}

//print(response)

RunLoop.main.run()
