//
//  Photo+Api.swift
//  MyFirstiOSApp
//
//  Created by David Mohundro on 3/28/18.
//  Copyright © 2018 David Mohundro. All rights reserved.
//

import then

extension Photo {
    static func fetchPhotos() -> Promise<[Photo]> {
        return api.fetchPhotos()
    }
}
