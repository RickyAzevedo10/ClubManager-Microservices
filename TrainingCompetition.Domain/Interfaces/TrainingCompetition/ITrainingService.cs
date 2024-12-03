using TrainingCompetition.Domain.DTOs;
using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Domain.Interfaces.Identity
{
    public interface ITrainingService
    {
        Task<TrainingSession?> DeleteTrainingSession(long id);
        Task<TrainingSession?> CreateTrainingSession(CreateTrainingSessionDTO trainingSessionBody);
        Task<TrainingSession?> UpdateTrainingSession(UpdateTrainingSessionDTO trainingSessionToUpdate, TrainingSession trainingSession);
        Task<TrainingAttendance?> CreateTrainingAttendance(CreateTrainingAttendanceDTO trainingAttendanceBody);
        Task<TrainingAttendance?> DeleteTrainingAttendance(long id);
        Task<TrainingAttendance?> UpdateTrainingAttendance(UpdateTrainingAttendanceDTO trainingAttendanceToUpdate, TrainingAttendance trainingAttendance);
    }
}