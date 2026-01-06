using System.Collections;

public class Employee
{
    public string EmployeeName { get; set; }
    public int EmployeeID { get; set; }
    public double Salary { get; set; }

    public Employee (int employeeID, string employeeName, double salary)
    {
        EmployeeID = employeeID;
        EmployeeName = employeeName;
        Salary = salary;
    }
    public void Display()
    {
        Console.WriteLine($"ID: {EmployeeID}, Name: {EmployeeName}, Salary: {Salary}");
    }   
}   

public class EmployeeDAL
{
    private ArrayList empList = new ArrayList();

  
    public bool AddEmployee(Employee e)
    {
        empList.Add(e);
        return true;
    }


    public bool DeleteEmployee(int id)
    {
        foreach (Employee e in empList)
        {
            if (e.EmployeeID == id)
            {
                empList.Remove(e);
                return true;
            }
        }
        return false;
    }


    public string SearchEmployee(int id)
    {
        foreach (Employee e in empList)
        {
            if (e.EmployeeID == id)
            {
                return e.EmployeeName;
            }
        }
        return null;
    }

    public Employee[] GetAllEmployeesListAll()
    {
        Employee[] arr = new Employee[empList.Count];
        empList.CopyTo(arr);
        return arr;
    }
}




