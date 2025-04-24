using AutoMapper;
using Backend.Controllers.Resources;
using Backend.Models;
using Backend.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("/user")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UserController(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper; 
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserResource userResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = _mapper.Map<UserResource, User>(userResource);
        
        var passwordHasher = new PasswordHasher<User>();
        user.Password = passwordHasher.HashPassword(user, userResource.Password);
        
        _userRepository.AddUser(user);
        await _unitOfWork.CompleteAsync();
        
        var result = _mapper.Map<User, UserResource>(user);
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userRepository.GetUserById(id, includeRelated: false);
        
        var result = _mapper.Map<User, UserResource>(user);
        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserResource userResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userRepository.GetUserById(id, includeRelated: false);

        _mapper.Map(userResource, user);
        await _unitOfWork.CompleteAsync();
        
        user = await _userRepository.GetUserById(id);
        var result = _mapper.Map<User, UserResource>(user);
        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserById(id, includeRelated: false);
        
        _userRepository.RemoveUser(user);
        await _unitOfWork.CompleteAsync();
        return Ok(id);
    }
}