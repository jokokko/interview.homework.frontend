# Background #

This package contains a backend that simulates four (4) microservices, each hosting a service for a product purchase flow.

The services with their respective ports are bootstrapped in `Program.Main`. They provide

* Product names
* Product prices
* Product thumbnails
* A shopping cart

All the services are accessed through HTTP, with API URIs of `/api/<product id>` (product id:int:optional), except for the shopping cart, which listens to `POST` and `DELETE` requests on `/api/<cart id>/<product id>`. One of the services also experiences degraded availability. This is indicated in its responses.

To run the backend, first install the latest .NET Core (https://www.microsoft.com/net/core). Then navigate to the `src/Demo.Backend` folder and in CLI execute `dotnet run` (the first compilation will also trigger a NuGet package restore).

Examine the backend as needed to discover any required modes or parameters for interaction, but do not modify the code.

# Tasks #

With the backend in place, implement a single-page frontend application using Angular (https://angular.io/). The frontend should

* Demonstrate client-side service composition, making use of the 4 microservices
    * Have a mitigation strategy for service unavailability
* Have the capability to display a product page, composed of
    * Product name
    * Product price
    * Product image
    * Add product to shopping cart functionality
* Have the capability to display a listing of all products with
    * Product names
    * Product images
    * Links to individual product pages
* Have the capability to display a shopping cart, composed of
    * Product names
    * Product prices
    * Shopping cart total price

A generalized, composable implementation is favoured.

Technical requirements

* Use RxJS to demonstrate the use of reactive streams
    * Explain the concept of hot and cold observables
* Briefly explain how change detection works in Angular and how changes propagate to components
* Calculate the shopping cart total price on the client side
* Generate the shopping cart id on the client side
    * One user per session can be assumed
* Write a unit test for one component or service

Additional 3rd party components for the frontend can be used. Availability of Web Storage can be assumed.

Additional points are awareded for

* Using custom form components (`ControlValueAccessor`)
* Making the unit test work without the real backend
* Writing an end-to-end test for one feature
    * Headless Chrome or Firefox can be used
* Explaining how Zones relate to change detection
* Explaining how the cart could be secured without storing user credentials