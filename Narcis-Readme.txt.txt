There weren't any specification on the matter, but I assumed that 2 concurrent requests will get queued while the machine is busy turning or advancing.

Build the project:
Open visual studio. Start to debug project SLMM.Api with IIS. 
	(Swagger page will be automatically open or visit: https://localhost:44320/swagger/index.html)

Nuget Packages:
I only used Swashbuckle for documentation and quick testing.

If I had  time:

- I would add logging
- I would define proper domain expcetions and error codes
- Convert machine.Position to machine.GetPositionAsync for the case that requirements might change and the position needs to be blocking until operation is finished.
- I haven't covered all unitests, so I would also focus on that.
- On the Api side, bare minimum was done, so I would validate request params (FluentValidation nuget). I would validate start up config and throw proper exceptions. Better Exception to Status Code (middleware)
- Use AutoFac or any mature DI implementation.
