//
//  PhotoCell+Photo.swift
//  MyFirstiOSApp
//
//  Created by David Mohundro on 3/28/18.
//  Copyright © 2018 David Mohundro. All rights reserved.
//

import Foundation
import Kingfisher

extension PhotoCell {
    func render(with p: Photo) {
        title.text = p.title
        photo.kf.setImage(with: p.imageUrl)
    }
}
