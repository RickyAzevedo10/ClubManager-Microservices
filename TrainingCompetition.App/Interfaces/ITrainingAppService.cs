using TrainingCompetition.Domain.DTOs;

namespace TrainingCompetition.Application.Interfaces
{
    public interface ITrainingAppService
    {
        Task<TrainingSessionResponseDTO?> DeleteTrainingSession(long id);
        Task<TrainingSessionResponseDTO?> GetTrainingSession(long trainingSessionId);
        Task<TrainingSessionResponseDTO?> CreateTrainingSession(CreateTrainingSessionDTO trainingSessionBody);
        Task<TrainingSessionResponseDTO?> UpdateTrainingSession(UpdateTrainingSessionDTO trainingSessionToUpdate);
        Task<TrainingAttendanceResponseDTO?> CreateTrainingAttendance(CreateTrainingAttendanceDTO trainingAttendanceBody);
        Task<TrainingAttendanceResponseDTO?> DeleteTrainingAttendance(long id);
        Task<TrainingAttendanceResponseDTO?> UpdateTrainingAttendance(UpdateTrainingAttendanceDTO trainingAttendanceToUpdate);
        Task<List<TrainingAttendanceResponseDTO>?> GetTrainingAttendance(long trainingAttendanceId);
        Task<List<TrainingSessionResponseDTO>?> GetTrainingSessionsByDateRange(DateTime startDate, DateTime endDate);
    }
}
