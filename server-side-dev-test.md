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

### Assessment

Your solution will be assessed based on:

1. Use of the language features
1. Readability and maintainability of code
1. Testing approach and methodology
