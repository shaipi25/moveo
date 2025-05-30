﻿using Dto;
using Gateway.Model.Queries;
using Requests;
using Responses;

namespace Services
{
    public interface ITasksService
    {
        public Task<TaskDto> Get(Guid projectId, Guid taskId, string userName);

        public Task<GetAllTasksResponse> GetAll(Guid projectId, GetAllTasksQuery query, string userName);

        public Task<TaskDto> Create(CreateTaskRequestDto request, string userName);

        public Task<TaskDto> Update(Guid projectId, Guid taskId, UpdateTaskRequestDto request, string userName);

        public void Delete(Guid projectId, Guid taskId, string userName);
    }
}
