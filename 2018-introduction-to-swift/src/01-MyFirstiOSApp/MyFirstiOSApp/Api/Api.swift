//
//  Api.swift
//  MyFirstiOSApp
//
//  Created by David Mohundro on 3/28/18.
//  Copyright © 2018 David Mohundro. All rights reserved.
//

import then

protocol ApiInterface {
    func fetchPhotos() -> Promise<[Photo]>
}

var api: ApiInterface!
