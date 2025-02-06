namespace TaskApi.Models;

public class TaskModel {
    public TaskModel(string taskName, string category, string priority, DateTime date){
        TaskName = taskName;
        Category = category;
        Priority = priority;
        Date = date;
        Id =  Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string TaskName { get; set; }
    public string Category { get; set; }
    public string Priority { get; set; }
    public DateTime Date { get; set; }

    public void ChangeTaskName(string taskName){
        TaskName = taskName;
    }

    public void ChangeCategory(string category){
        Category = category;
    }

    public void ChangePriority(string priority){
        Priority = priority;
    }

    public void ChangeDate(DateTime date){
        Date = date;
    }

    public void SetFinish(){
        TaskName = "Concluida";
    }
}