﻿using Microsoft.EntityFrameworkCore;
using System;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public class LessonService : ILessonService
    {
        private DefaultDBContext _dbContext;

        public LessonService(DefaultDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddLesson(LessonDTOModel model)
        {
            if (model.EndDate < model.StartDate)
            {
                throw new ArgumentException("Wrong Dates!");
            }
            var lesson = new LessonDbModel
            {
                Id = new Guid(),
                TimeSlot = model.TimeSlot,
                StartDate = DateOnly.FromDateTime(model.StartDate),
                TeacherId = model.TeacherId,
                RoomId = model.RoomId,
                GroupId = model.GroupId,
                SubjectId = model.SubjectId,
                EndDate =  model.EndDate==null ? DateOnly.MaxValue : DateOnly.FromDateTime((DateTime)model.EndDate).AddDays(1),
                WeekDay = (int)DateOnly.FromDateTime(model.StartDate).DayOfWeek
            };
            if (await _dbContext.CheckIfCanBeAddedInDatabase(lesson))
            {
                await _dbContext.Lessons.AddAsync(lesson);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("There is already lesson on this slot!");
            }
        }

        public async Task DeleteLesson(Guid lessonId)
        {

            var lesson = _dbContext.Lessons.FirstOrDefault(x => x.Id == lessonId);
            if (lesson == null)
            {
                throw new Exception("There is no lesson with this ID!");
            }
            lesson.EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-1);
            if (lesson.EndDate <= lesson.StartDate)
            {
                _dbContext.Lessons.Remove(lesson);
            }       
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditLesson(Guid lessonId, LessonEditDTOModel model)
        {

            var lesson = _dbContext.Lessons.FirstOrDefault(x => x.Id == lessonId);          
            if (lesson == null)
            {
                throw new RankException("There is no lesson with this ID!"); //TODO: change RankException
            }
            else {
                var newLesson = new LessonDbModel
                {
                    Id = lessonId,
                    StartDate = model.StartDate == null ? lesson.StartDate : DateOnly.FromDateTime((DateTime)model.StartDate),
                    TimeSlot = model.TimeSlot == null ? lesson.TimeSlot : (int)model.TimeSlot,
                    TeacherId = model.TeacherId == null ? lesson.TeacherId : (Guid)model.TeacherId,
                    RoomId = model.RoomId == null ? lesson.RoomId : (Guid)model.RoomId,
                    GroupId = model.GroupId == null ? lesson.GroupId : (Guid)model.GroupId,
                    SubjectId = model.SubjectId == null ? lesson.SubjectId : (Guid)model.SubjectId,
                    WeekDay = model.StartDate == null ? lesson.WeekDay : (int)DateOnly.FromDateTime((DateTime)model.StartDate).DayOfWeek,
                    EndDate = model.EndDate == null ? lesson.EndDate : DateOnly.FromDateTime((DateTime)model.EndDate).AddDays(1)
                };
                if (newLesson.EndDate < newLesson.StartDate)
                {
                    throw new ArgumentException("Wrong Dates!");
                }
                _dbContext.Lessons.Remove(lesson);
                await _dbContext.SaveChangesAsync();
                if (await _dbContext.CheckIfCanBeAddedInDatabase(lesson))
                {
                    _dbContext.Lessons.Add(newLesson);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _dbContext.Lessons.Add(lesson);
                    await _dbContext.SaveChangesAsync();
                    throw new InvalidOperationException("There is already lesson on this slot!");
                }
            }
        }
    }
}