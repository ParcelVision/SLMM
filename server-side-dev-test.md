SLMM - The coding exercise for server side applicants
=====================================================

### Short description
Assume we are interested in creating a smart lawn mowing machine called SLMM (smart lawn mowing machine).
The SLMM operates in a rectangular garden that is a grid with dimensions `Length x Width`; SLMM can move forward
to the next grid cell or turn 90° clockwise or anticlockwise.

Your task is to create the software that will run in the SLMM itself and will be responsible for doing the following actions:

1. Turn the SLMM 90° clockwise or anticlockwise -> This should take 2 seconds to do
1. Move forward by one position -> This takes 5 seconds to do

To emulate work being done, please use `Sleep` or `Delay`.

You are expected to create a web API that will accept the above commands, and execute them. During application startup (through config) or through a "reset" endpoint, the SLMM is given dimensions `(length,width)` of the garden where it operates. You can assume an initial position of `(0,0)` (which is the lower left and south-western corner of the rectangle) and that the lawn mower is facing north.

UI is not required for this exercise, you can use Postman, curl, or similar client to access the API.

Please try to not take more than 3-4 hours total on this exercise. Please take a simple approach to this, and do not overengineer your solution. If you're taking more than the indicated time, you're doing too much work. We will only be assesing your use of the language, readability and testing. You can assume that there's only ever one lawn mower (no need for concurrency or resource management), and while we want to see how you allocate responsibilities, we are not going to be evaluating the software architecture of the solution.

### Actions to support
1. Turn 90° clockwise
1. Turn 90° anticlockwise
1. Move one step forward
1. Get current position using `x` and `y` coordinated to indicate the position of the lawn mower in the rectangular garden (which will be the coordinates of the grid cell), and `orientation` (one of `North`/`East`/`South`/`West`)

### Deliverable
A web API implemented in C# using any web framework of your choice. Automated tests must be included in the delivery. You are free to use any nuget library you choose.

Please include a short documentation explaining how to build, run and use the solution you authored. Please add a section about any decisions you made during the implementation. If you used any library other than for DI, testing or web framework, then please add a small section explaining why.

### Acceptance criteria

The provided solutions needs to build with no errors. Feel free to use any 3rd party libraries you chose, but aside from web framework and DI libraries, please explain clearly your choices.

1. The SLMM never goes outside of the dimensions of the garden as supplied during startup.
1. The SLMM web API remains responsive

> **NOTE**: If you don't manage to finish all requirements within 2-3 hours, this isn't a problem. As long as allocation of responsibilities, your testing approach and use of testing can be assessed, it's OK to submit.

### Assessment

Your solution will be assessed based on:

1. Allocation of responsibilities to components
1. Use of language features (and built-in collections)
1. Testing approach and methodology

Please do:
- Use any nuget library that can help you deliver this quickly as long as the above can be demonstrated
- Try to finish within 3 hours at most. It is more than enough time to demonstrate the above assessment points
- Submit your application even if not all acceptance criteria are finished.
- Use any scaffolding tools you want to create the solution
- Use _any_ testing framework you want and are familiar with
- Use appropriate testing for the solution and it's size

Please do not:
- Provide a UI for this application. We do not need one and it will not count as a positive. You will only spend more time
- Demonstrate knowledge of CQRS or any other architectural pattern (or messaging or Event Sourcing for that matter). We do use them in our system, but we will be assessing your application solely on the points mentioned above.
- Spend too long on your solution. This could be done as quickly as 1.5 hours and demonstrate the above. Please don't take more than 3 hours.
