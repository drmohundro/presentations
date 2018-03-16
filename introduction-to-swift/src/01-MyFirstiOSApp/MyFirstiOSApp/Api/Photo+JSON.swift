//
//  Photo+JSON.swift
//  MyFirstiOSApp
//
//  Created by David Mohundro on 3/28/18.
//  Copyright © 2018 David Mohundro. All rights reserved.
//

import Arrow

extension Photo : ArrowParsable {    
    public mutating func deserialize(_ json: JSON) {
        identifier <-- json["id"]
        title <-- json["title"]
        imageUrl <-- json["url"]
    }
}
