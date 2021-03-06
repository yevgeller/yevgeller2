﻿using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using yevgeller2.DTOs;
using yevgeller2.Models;

namespace yevgeller2.Controllers.api
{
    [Authorize]
    public class ProjectsApiController : ApiController
    {
        ApplicationDbContext _db;
        private string userId;

        public ProjectsApiController()
        {
            userId = User.Identity.GetUserId() ?? "temp user Id";
            _db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult RecordTag(TagDto tagDto)
        {
            TempStorageTag tst = new TempStorageTag
            {
                IdNo = tagDto.IdNo,
                Name = tagDto.TagName,
                Action = tagDto.Action,
                TimeStamp = DateTime.Now,
                UserId = userId
            };

            _db.TempStorageTags.Add(tst);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult RemoveTag(TagDto tagDto)
        {
            TempStorageTag tst = _db.TempStorageTags
                .Where(x => x.Name == tagDto.TagName
                        && x.UserId == userId
                        && x.IdNo == tagDto.IdNo)
                //                        && x.Action == ProjectAction.Create)
                .FirstOrDefault();

            if (tst == null) return BadRequest("No such temp tag");

            _db.Entry(tst).State = EntityState.Deleted;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ToggleProjectVisibilityStatus(int id)
        {
            Project p = _db.Projects
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (p != null)
            {
                p.IsHidden = !p.IsHidden;
            }
            _db.Entry(p).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public HttpResponseMessage RecordActivityPeriod(ActivityDurationRecordDTO adr)
        {
            if (User.Identity.IsAuthenticated == false)
                return Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);

            DateTime a = DateTime.ParseExact(adr.StartDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime z = DateTime.ParseExact(adr.EndDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            
            ActivityDurationRecord record = new ActivityDurationRecord
            {
                StartDateTime = Convert.ToDateTime(adr.StartDateTime),
                EndDateTime = Convert.ToDateTime(adr.EndDateTime),
                DurationInSeconds = adr.DurationInSeconds,
                RecordType = adr.RecordType,
                UserId = userId
            };

            _db.ActivityDurationRecords.Add(record);
            _db.SaveChanges();
            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }
    }
}
