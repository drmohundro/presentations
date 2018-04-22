//
//  MainVC.swift
//  MyFirstiOSApp
//
//  Created by David Mohundro on 3/28/18.
//  Copyright © 2018 David Mohundro. All rights reserved.
//

import UIKit

class PhotosVC: UIViewController {
    var v = PhotosView()
    override func loadView() { view = v }
    
    // We need to store an array of Photos
    var photos = [Photo]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        v.refreshControl.addTarget(self, action: #selector(refresh), for: .valueChanged)
        
        // After the view loads, we set ourselves as the delegate of the Tableview.
        v.tableView.dataSource = self
        
        // And we fetch the photos.
        refresh()
    }
    
    @objc
    func refresh() {
        // Get the full documentation at https://github.com/freshOS/then
        Photo.fetchPhotos().then { fetchedPhotos in
            // Yay, we got our photos !
            self.photos = fetchedPhotos
            }.onError { e in
                // An error occured :/
                print(e)
            }.finally {
                // In any case, reload the tableView
                self.v.tableView.reloadData()
                self.v.refreshControl.endRefreshing()
        }
    }
}

extension PhotosVC: UITableViewDataSource {
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return photos.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        if let cell = tableView.dequeueReusableCell(withIdentifier: "PhotoCell", for: indexPath) as? PhotoCell {
            let photo = photos[indexPath.row]
            cell.render(with: photo) // Here we use a render helper to keep our code that populates the
            // Cell separated, this keeps things nice and clean.
            return cell
        }
        return UITableViewCell()
    }
}
