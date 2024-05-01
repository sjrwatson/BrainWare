# Change log :book:

- Order controller clean up.
- Database changed to interface, so it can be mocked more easily for testing.
- Order service clean up, data access responsibilities moved to new classes OrderDataAccess and OrderProductDataAccess.
- Order model cleanup, set defaults to prevent warnings.
- Program.cs updated to inject new classes via interface, enabling mocking for better test coverage.
- New tests for OrderController.
- New tests for OrderService.
- New tests for data access classes OrderDataAccess and OrderProductDataAccess.
- Migrated to latest version of NX, this was to resolve security vulnerabilities flagged by npm install.
- Split ui into multiple relevant components and service for orders.
- Tests added for each component.
- Angular cli updated to match angular version.
- Removed unused styles.
- Added logging.

##Also ran Snyk scan to check for vulnerabilities, 2 found:

:grey_exclamation: Low severity - Improper Restriction of Rendered UI Layers or Frames<br>
:exclamation:  Med severity - Missing Release of Resource after Effective Lifetime, introduced by angular<br>
:fire:  No high severity  issues<br><br>

No high priority security risks uncovered.

# Notes :books:

## Code structure :moyai:

Code was functional, most changes that I made were to make the code more modular for testing.

## Test coverage :vertical_traffic_light:

I feel that I got quite good test coverage after addressing the app structure.
This would enable us to have quality gates set to a high standard on CI/CD pipelines, for any future development and deployments.

## Validation :white_check_mark:

I did not feel the need to add any validation as the api was only retrieving data and not saving or modifying in any way.

## Logging :memo:

In a real production setting I would like to do this slightly differently.
I set up logging to log straight to the console, this makes the assumption that we would have access to console logs wherever this was hosted.
In a production setting I would like to use something like seq where we could log to a central server.
This would enable us to query for issues on a regular basis from a centralised UI.
And monitor any further issues which may arise.

## Security :lock:

I ran a Snyk analysis on the github repository when I was at a point of being happy with my changes.
Thankfully no major vulnerabilities were flagged.
I also updated to the latest version of Angular as NPM flagged vulnerabilities during install.

## Containerisation :gift:



## Performance :hourglass:

I did not perform any performance tuning on this app.
If post release there where issues with performance there is an opportunity for parallelism when getting orders and order products from the DB.
