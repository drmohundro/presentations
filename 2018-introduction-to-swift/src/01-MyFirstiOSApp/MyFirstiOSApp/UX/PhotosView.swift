//
//  MainView.swift
//  MyFirstiOSApp
//
//  Created by David Mohundro on 3/28/18.
//  Copyright © 2018 David Mohundro. All rights reserved.
//

import Stevia

class PhotosView: UIView {
    
    let refreshControl = UIRefreshControl()
    let tableView = UITableView()
    
    convenience init() {
        self.init(frame:CGRect.zero)
        
        // Here we use Stevia to make our constraints more readable and maintainable.
        sv(tableView)
        tableView.fillContainer()
        
        tableView.addSubview(refreshControl)
        
        // Configure Tableview
        tableView.register(PhotoCell.self, forCellReuseIdentifier: "PhotoCell") // Use PhotoCell
        tableView.estimatedRowHeight = 200 // Enable self-sizing cells
    }
}
