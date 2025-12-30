//
//  WSApi.swift
//  MyFirstiOSApp
//
//  Created by David Mohundro on 3/28/18.
//  Copyright © 2018 David Mohundro. All rights reserved.
//

import ws
import then

class WSApi: ApiInterface {
    
    let ws = WS("https://jsonplaceholder.typicode.com") // Set the Webservice base URL
    
    init() {
        // This will print network requests & responses to the console.
        ws.logLevels = .debug
    }
    
    // Set the type you want back and call the endpoint you need.
    func fetchPhotos() -> Promise<[Photo]> {
        print("Fetching photos...")
        return ws.get("/photos")
    }
}
