using APIExtension.ClaimsPrinciple;
using APIExtension.Const;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ShareResource.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private IRepoWrapper repos;
        private IMapper mapper;

        public ReviewsController(IRepoWrapper repos, IMapper mapper)
        {
            this.repos = repos;
            this.mapper = mapper;
        }


        [Authorize(Roles=Actor.Student)]
        [HttpGet("Meeting/{meetingId}")]
        public async Task<IActionResult> GetReviewForMeeting(int meetingId)
        {
            string reviewee = HttpContext.User.GetUsername();
            var list = repos.Reviews.GetList()
                            .Where(e => e.MeetingId == meetingId)
                            .Include(e => e.Reviewee)
                            .Include(e => e.Details).ThenInclude(e => e.Reviewer);
            var mapped = list
                .ProjectTo<ReviewSignalrDTO>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }
        
        [SwaggerOperation(
            Summary = $"[{Actor.Test}/{Finnished.True}] Login for student with username or email. Return JWT Token"
            //,Description = "Login for student with username or email. Return JWT Token if successfull"
        )]
        [Authorize(Roles=Actor.Student)]
        [HttpGet("Start")]
        public async Task<IActionResult> StartReviewForUserInMeeting(int meetingId)
        {
            int revieweeId = HttpContext.User.GetUserId();
            Review newReview = new Review
            {
                MeetingId = meetingId,
                RevieweeId = revieweeId
            };
            await repos.Reviews.CreateAsync(newReview);
            ReviewSignalrDTO mapped = mapper.Map<ReviewSignalrDTO>(newReview);
            return Ok(mapped);
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Test}/{Finnished.False}] Vote for a student review"
        //,Description = "Login for student with username or email. Return JWT Token if successfull"
        )]
        [Authorize(Roles=Actor.Student)]
        [HttpPost("Vote")]
        public async Task<IActionResult> StartReviewForUserInMeeting(ReviewDetailSignalrCreateDto dto)
        {
            int reviewerId = HttpContext.User.GetUserId();
            ReviewDetail newReviewDetail = new ReviewDetail
            {
                ReviewId = dto.ReviewId,
                Comment = dto.Comment,
                Result = dto.Result,
                ReviewerId = reviewerId,
            };
            await repos.ReviewDetails.CreateAsync(newReviewDetail);
            ReviewDetailSignalrGetDto mapped = mapper.Map<ReviewDetailSignalrGetDto>(newReviewDetail);
            return Ok(mapped);
        }
    }
}
