# Automating Deployments

<..>

## Overview of our deployments

* We have a folder of deployments on each web server
  * It is basically a sliding window of the last 3 deployments
  * Each folder has its own deployment script
  * This allows for the addition of new steps in the deployment
* *Everything* that is part of a deployment gets configured
  * Web sites
  * Windows Services
  * GAC installs
  * COM+ libraries
  * etc.

<..>

## Step One - VALIDATE

* The *FIRST* step is to ensure no active connections are hitting your site
  * If you care at all about uptime, don't even think about deploying to an active site
  * For us, that means checking the F5 to make sure the server is inactive

<..>

## Step Two - Point to new Deployment

* You can do this multiple ways
* For example, with our Windows Services, we uninstall the old ones (via the prior deployment folder)
* Then we install from the new deployment

<..>

## Step Three - Make it Repeatable

* Make it so that you can run the same deployment script multiple times in a row and not break anything
* Think of it like a unit test... each deployment is a unit and shouldn't have any dependency on the prior deployment (aside from uninstall the prior deployment)

<aside class="notes" data-markdown>
* want to see?
</aside>
