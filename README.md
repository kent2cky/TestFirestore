# TestFirestore

This project was created specifically to test out why firestore datastore is showing "Permission Denied" when attempt to control from the backend service account.

## To run this project

- clone the project
- run dotnet restore
- run dotnet run
- go to localhost:????/WeatherForecast to trigger the code that sends request to firestore. If the request is successfull then you should get 20x response otherwise you'll get an exception

## Other information

- you will find that the credentials of the service account is on the project.
