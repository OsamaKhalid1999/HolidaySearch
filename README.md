 Project Overview

This project fulfills all functional requirements outlined for the Holiday Search application. It supports searching for holiday packages by filtering flights and hotels based on user-specified criteria such as departure location, destination, travel date, and duration. Results are paginated and sorted by total price.

How to Run

1. Clone the repository  
   Clone the public repository to your local machine using:

   git clone https://github.com/OsamaKhalid1999/HolidaySearch.git

2. Build and run the application  
   Open the solution in your preferred IDE and run the application. The Swagger UI will be available for testing the API.

3. Test with Sample Payloads  
   In the Swagger UI, use the POST /search endpoint and test with any of the following request bodies:

   {
     "departingFrom": "MAN",
     "travelingTo": "AGP",
     "departureDate": "2023-07-01T00:00:00.000Z",
     "duration": 7,
     "pageNumber": 1,
     "pageSize": 10
   }

   {
     "departingFrom": "London",
     "travelingTo": "PMI",
     "departureDate": "2023-06-15T00:00:00.000Z",
     "duration": 10,
     "pageNumber": 1,
     "pageSize": 10
   }

   {
     "departingFrom": "Any Airport",
     "travelingTo": "LPA",
     "departureDate": "2022-11-10T00:00:00.000Z",
     "duration": 14,
     "pageNumber": 1,
     "pageSize": 10
   }

   These inputs will return paginated holiday search results matching the given criteria, ordered by total price.

Design Overview

A flowchart diagram is included in the repository to illustrate the overall search logic. It was used during the initial planning phase to ensure a clear understanding of the business flow and technical requirements.