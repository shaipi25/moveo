﻿using Dto;
using moveo.Requests;

namespace Services
{
    public interface IProjectsService
    {
        public Task<ProjectDto> GetAsync(Guid projectId, string userName);

        public Task<List<ProjectDto>> GetAllAsync(int? pageNumber, int? pageSize, string userName);

        public Task<ProjectDto> CreateAsync(CreateProjectRequest request, string userName);

        public Task<ProjectDto> UpdateAsync(Guid projectId, UpdateProjectRequest request, string userName);

        public Task Delete(Guid projectId, string userName);
    }
}