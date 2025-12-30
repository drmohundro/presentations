//
//  PhotoCell.swift
//  MyFirstiOSApp
//
//  Created by David Mohundro on 3/28/18.
//  Copyright © 2018 David Mohundro. All rights reserved.
//

import Stevia

class PhotoCell: UITableViewCell {
    
    let title = UILabel()
    let photo = UIImageView()
    
    required init?(coder aDecoder: NSCoder) { super.init(coder: aDecoder)}
    override init(style: UITableViewCellStyle, reuseIdentifier: String?) {
        super.init(style: style, reuseIdentifier: reuseIdentifier)
        
        // This adds our views to the cell's content view as needed
        // and prepares them for autolayout use.
        // This has the advantage of being very visual. Indeed, sv calls can be nested
        // and you see the view hierarchy right away.
        sv(
            title,
            photo
        )
        
        // Here we layout the cell.
        layout(
            16,
            |-16-title-16-|,
            16,
            |-16-photo-16-|,
            16
        )
        photo.heightEqualsWidth()
        
        // Configure visual elements
        title.numberOfLines = 0
        photo.backgroundColor = .lightGray
    }
}
