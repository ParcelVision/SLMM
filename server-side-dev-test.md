SLMM - The coding exercise for server side applicants
=====================================================

Please do not overengineer this exercise, but at the same time try to demonstrate knowledge of good code design principles.

### Short description
Assume we are interested in creating a smart lawn mowing machine called SLMM (smart lawn mowing machine).
The SLMM operates in a rectangular garden that is a grid with dimensions `Length x Width`; SLMM can move forward
to the next grid cell or turn 90° clockwise or anticlockwise.

Your task is to create the software that will run in the SLMM itself and will be responsible for doing the following actions:

1. Turn the SLMM 90° clockwise or anticlockwise -> This should take 2 seconds to do
1. Move forward by one position -> This takes 5 seconds to do

To emulate work being done, please use `Sleep`.

You are expected to create a web API that will accept the above commands, and execute them. During application startup,
the SLMM is given dimensions `(length,width)` of the garden where it operates, and initial position `(x,y,orientation)` - location in the garden (grid cell coordinates) and orientation (`North`/`East`/`South`/`West`). These settings can be passed in through a configuration file.

UI is not required for this exercise, you can use Postman, curl, or similar client to access the API.

Please try to not take more than 3-4 hours total on this exercise.

### Actions to support
1. Turn 90° clockwise
1. Turn 90° anticlockwise
1. Move one step forward
1. Get current position `(x,y,orientation)` - location in the garden (grid cell coordinates) and orientation (`North`/`East`/`South`/`West`)

### Deliverable
A web API implemented in C# using any web framework of your choice. Automated tests must be included in the delivery. You are free to use any nuget library you choose.

Please include a short documentation explaining how to build, run and use the solution you authored. Please add a section about any decisions you made during the implementation. If you used any library other than for DI, testing or web framework, then please add a small section explaining why.

### Acceptance criteria

The provided solutions needs to build with no errors. Feel free to use any 3rd party libraries you chose, but aside from web framework and DI libraries, please explain clearly your choices.

1. The SLMM never goes outside of the dimensions of the garden as supplied during startup.
1. The SLMM web API remains responsive - get current position returns a value immediately regardless if the SLMM is busy rotating or moving forward.
1. When queried the SLMM should return the current position until it finished moving.

### Assessment

Your solution will be assessed based on:
1. Use of the language
1. Readability and maintainability of code
1. Adhering to coding standards
1. Allocating responsibilities to classes
1. Testing approach and methodology
