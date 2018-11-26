Assumptions:
- Lawnmower cannot receive additional requests while an operation is in progress.
	- If otherwise required, a queue can be added with additional position plotting validation.
- API is kept responsive for Get Status, all other operations are blocking. This was based on my understanding of the requirements.

	
Decisions:
- ILawnMower/VirtualLawnMower - serves as the device interface only. 
Responsibilities: turning and advancing the mower. Has no spatial responsibilities. 
Ensures that only one operation can be performed at a time.
- Map - Supports rectangular maps and pin navigation. Keeps track of pins and validates navigation.
- ILawnMowerDrive/VirtualLawnMowerDriver - responsible for operating a ILawnMower. Keeps track of Map and Orientation.
- For testing: Tested basic specifications and validation. Did not require any mocking since this was a simple solution.
- For the API interface: Since this is a simple solution there was no need for additional DTOs, mapping or validation.



Development Setup:
- Required: Visual Studio 2017 with .NET Core 2.1 SDK installed
- Open SLMM.sln and run the SLMM Web Api Project (hosted in IISExpress)
- Postman/CURL test:
	- GET https://localhost:44303/api/LawnMower - Retrieves LawnMower status
	- POST https://localhost:44303/api/LawnMower/advance - Advances the LawnMower by one unit
	- POST https://localhost:44303/api/LawnMower/turn?direction=Clockwise - Turns the LawnMower

Future thoughts:
- Add logging
- Include virtual device tracking in persistent storage
- Current API responds with a BadRequest while an operation is in progress or mower cannot advance.
 Would like to add proper domain exceptions that map to proper ModelState errors or HTTP status codes.
