using RentACar.controller;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RentACar.validation
{
    internal class Validation
    {
        public static int validateUniqueId<T>(int id, List<T> items) where T : class
        {
            if (items.Count > 0 || id < 1) {
                bool unique;
                do
                {
                    unique = true;

                    foreach (var elem in items)
                    {
                        var idValue = (int)elem.GetType().GetMethod("getId").Invoke(elem, null);
                        if (id == idValue || id < 1)
                        {
                            unique = false;
                            break;
                        }
                    }

                    if (unique)
                    {
                        return id;
                    }
                    else
                    {
                        Console.Write("Invalid input! Re-enter id: ");
                        id = Validation.getValidInt();
                    }
                } while (!unique); // could be while (true);
            }
            return id;
        }

        public static int validateExistingId<T>(int id, List<T> items) where T : class
        {
            if (items.Count > 0)
            {
                bool existing;
                do
                {
                    existing = false;
                    foreach (var elem in items)
                    {
                        var idValue = (int)elem.GetType().GetMethod("getId").Invoke(elem, null);
                        if (id == idValue)
                        {
                            existing = true;
                            break;
                        }
                    }

                    if (existing)
                    {
                        return id;
                    }
                    else
                    {
                        Console.Write("Invalid input! Re-enter id: ");
                        id = Validation.getValidInt();
                    }
                } while (!existing); // could be while (true);
            }
            return 0;
        }

        public static string validateProperNoun(string properNounValue, string properNoun)
        {
            // Regex proper nouns
            string pattern = @"^([A-Z]([a-z]{1,20})?(_|-)?){1,5}$";
            Match match = Regex.Match(properNounValue, pattern);
            string newProperNounValue = properNounValue;
            while (!match.Success)
            {
                Console.Write($"Invalid input! Re-enter a new {properNoun}: ");
                newProperNounValue = Console.ReadLine();
                match = Regex.Match(newProperNounValue, pattern);
            }
            return newProperNounValue;
        }

        public static string validateCommonNoun(string commonNounValue, string commonNoun)
        {
            // Regex common nouns
            string pattern = @"^[a-z]{1,20}$";
            Match match = Regex.Match(commonNounValue, pattern);
            string newCommonNounValue = commonNounValue;
            while (!match.Success)
            {
                Console.Write($"Invalid input! Re-enter a new {commonNoun}: ");
                newCommonNounValue = Console.ReadLine();
                match = Regex.Match(newCommonNounValue, pattern);
            }

            return newCommonNounValue;
        }
#if false
        public static string validateCountry(string country)
        {
            // Regex country
            string pattern = @"^([A-Z]([a-z]{1,20})?(_|-)?){1,5}$";
            Match match = Regex.Match(country, pattern);
            string newCountry = country;
            while (!match.Success)
            {
                Console.Write($"Invalid input! Re-enter a new country: ");
                newCountry = Console.ReadLine();
                match = Regex.Match(newCountry, pattern);
            }

            return newCountry;
        }
#endif
        public static string validateGender(string gender)
        {
            // Regex gender
            string pattern = @"^(M|m|Male|male|F|f|Female|female)$";
            Match match = Regex.Match(gender, pattern);
            string newGender = gender;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new gender: ");
                newGender = Console.ReadLine();
                match = Regex.Match(newGender, pattern);
            }

            return newGender;
        }

        public static string validateEmployeePosition(string position)
        {
            // Regex position
            string pattern = @"^[A-Z][a-z]{1,20}((_|-)[a-z]{1,15})?$";
            Match match = Regex.Match(position, pattern);
            string newPosition = position;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new position: ");
                newPosition = Console.ReadLine();
                match = Regex.Match(newPosition, pattern);
            }
            return newPosition;
        }

        public static string validateEmail(string email)
        {
            // Regex email
            string pattern = @"^[A-Za-z]{1}[A-Za-z0-9.+_-]{3,}@(gmail|mail|outlook|yahoo|example|domain|subdomain)" +
                              "(.uk|.ru|.com|.ro|.org|.co|.au|.co|.net|.ca|.io|.domain)" +
                              "(.uk|.ru|.com|.ro|.org|.co|.au|.co|.net|.ca|.io)?$";
            Match match = Regex.Match(email, pattern);
            string newEmail = email;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new email: ");
                newEmail = Console.ReadLine();
                match = Regex.Match(newEmail, pattern);
            }
            return newEmail;
        }

        public static int validateClientAge(int age)
        {
            if (age >= 19 && age <= 75)
            {
                return age;
            }
            else
            {
                do
                {
                    Console.Write("Invalid input! Re-enter employee's age: ");
                    age = getValidInt();
                } while (age < 19 || age > 75);

                return age;
            }
        }

        public static int validateAge(int age)
        {
            if (age >= 18 && age <= 75)
            {
                return age;
            }
            else
            {
                do
                {
                    Console.Write("Invalid input! Re-enter employee's age: ");
                    age = getValidInt();
                } while (age < 18 || age > 75);

                return age;
            }
        }

        public static int validateEmployeeWorkExperience(int workExperience, int age)
        {
            if (workExperience >= 0 && workExperience <= age - 18)
            {
                return workExperience;
            }
            else
            {
                do
                {
                    Console.Write("Invalid input! Re-enter employee's work experience(years): ");
                    workExperience = getValidInt();
                } while (workExperience < 0 || workExperience > age - 18);

                return workExperience;
            }
        }

        public static int validateEmployeeSalary(int salary)
        {
            if (salary >= 300)
            {
                return salary;
            }
            else
            {
                do
                {
                    Console.Write("Invalid input! Re-enter employee's salary($$): ");
                    salary = getValidInt();
                } while (salary < 300);

                return salary;
            }
        }

        public static int getValidInt()
        {
            int variableToInt = 0;
            while (true)
            {
                try
                {
                    variableToInt = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.Write("You've entered not an integer value! Try again: ");
                }
            }

            return variableToInt;
        }

        public static char getValidChar()
        {
            char variableToChar = '\0';
            while (true)
            {
                try
                {
                    variableToChar = Console.ReadLine()[0];
                    break;
                }
                catch (Exception ex)
                {
                    Console.Write("You've entered not a char value! Try again: ");
                }
            }

            return variableToChar;
        }

        public static bool getValidBool()
        {
            bool variableToBool = false;
            while (true)
            {
                try
                {
                    variableToBool = Convert.ToBoolean(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.Write("You've entered not a boolean value! Try again: ");
                }
            }
            return variableToBool;
        }

        public static string getValidStringDate()
        {
            DateTime variableToDate;
            while (true)
            {
                string format = "dd/MM/yyyy";
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, format, null, DateTimeStyles.None, out variableToDate) 
                                          && variableToDate.Year <= DateTime.Now.Year && variableToDate.Year > 2010)
                {
                     return variableToDate.ToShortDateString();
                }
                else
                {
                    Console.Write($"Invalid date! Please enter a valid date in the format '{format}': ");
                }
            }
        }

        public static bool validateStringToBool(string stringObjectName)
        {
            // Regex validate string to bool
            string stringToBool = Console.ReadLine();
            string pattern = @"^(yes|no|y|n|Y|N|Yes|No)$";
            Match match = Regex.Match(stringToBool, pattern);
            string newStringToBool = stringToBool;
            while (!match.Success)
            {
                Console.Write($"Invalid input! Re-enter a new {stringObjectName}: ");
                newStringToBool = Console.ReadLine();
                match = Regex.Match(newStringToBool, pattern);
            }
            return newStringToBool.StartsWith("y", StringComparison.OrdinalIgnoreCase) ? true : false;
        }

        public static string validateIdnp(string idnp)
        {
            // Regex idnp
            string pattern = @"^[1-9][0-9]{12}$";
            Match match = Regex.Match(idnp, pattern);
            string newIdnp = idnp;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new idnp: ");
                newIdnp = Console.ReadLine();
                match = Regex.Match(newIdnp, pattern);
            }
            return newIdnp;
        }

        public static string validatePhoneNumber(string phoneNumber)
        {
            // Regex phone number
            string pattern = @"^\+373(6|7)[0-9]{7}$";
            Match match = Regex.Match(phoneNumber, pattern);
            string newPhoneNumber = phoneNumber;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new phone number: ");
                newPhoneNumber = Console.ReadLine();
                match = Regex.Match(newPhoneNumber, pattern);
            }
            return newPhoneNumber;
        }

        public static int validateClientDrivingExperience(int drivingExperience, int age)
        {
            if (drivingExperience >= 1 && drivingExperience <= age - 18)
            {
                return drivingExperience;
            }
            else
            {
                do
                {
                    Console.Write("Invalid input! Re-enter client's driving experience(years): ");
                    drivingExperience = getValidInt();
                } while (drivingExperience < 1 || drivingExperience > age - 18);

                return drivingExperience;
            }
        }

        public static string validateMake(string make)
        {
            // Regex make
            string pattern = @"^[A-Z]([A-Z]{2,}|[a-z]{2,})(-|_)?([A-Z]([A-Z]{2,}|[a-z]{2,}))?$";
            Match match = Regex.Match(make, pattern);
            string newMake = make;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new make: ");
                newMake = Console.ReadLine();
                match = Regex.Match(newMake, pattern);
            }
            return newMake;
        }

        public static string validateModel(string model)
        {
            // Regex model
            string pattern = @"^[A-Z1-9]([_|-]?([A-Za-z0-9]{1,})){1,3}$";
            Match match = Regex.Match(model, pattern);
            string newModel = model;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new model: ");
                newModel = Console.ReadLine();
                match = Regex.Match(newModel, pattern);
            }
            return newModel;
        }

        public static string validateTransmissionType(string transmissionType)
        {
            // Regex transmission
            string pattern = @"^(manual|automatic|cvt|amt|evt|cvat)$";
            Match match = Regex.Match(transmissionType, pattern);
            string newTransmissionType = transmissionType;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new transmission type: ");
                newTransmissionType = Console.ReadLine();
                match = Regex.Match(newTransmissionType, pattern);
            }
            return newTransmissionType;
        }

        public static int validateCarDistanceTraveled(int distanceTraveled)
        {
            if (distanceTraveled >= 0 && distanceTraveled < 300000)
            {
                return distanceTraveled;
            }
            else
            {
                do
                {
                    Console.Write("Invalid input! Re-enter car's traveled distance: ");
                    distanceTraveled = getValidInt();
                } while (distanceTraveled < 0 || distanceTraveled > 300000);

                return distanceTraveled;
            }
        }

        public static int validateCarYear(int year)
        {
            if (year >= 1990 && year <= DateTime.Now.Year)
            {
                return year;
            }
            else
            {
                do
                {
                    Console.Write("Invalid input! Re-enter car's year: ");
                    year = getValidInt();
                } while (year < 1990 || year > DateTime.Now.Year);

                return year;
            }
        }

        public static string validateDescription(string description)
        {
            // Regex gender
            string pattern = @"^[A-Z][a-z]{1,20}((_|-)?([A-Z])?[a-z]{1,20}){1,40}$";
            Match match = Regex.Match(description, pattern);
            string newDescription = description;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new description status: ");
                newDescription = Console.ReadLine();
                match = Regex.Match(newDescription, pattern);
            }

            return newDescription;
        }

        public static string validateGravityStatus(string gravityStatus)
        {
            // Regex gender
            string pattern = @"^(Low|Medium|High)$";
            Match match = Regex.Match(gravityStatus, pattern);
            string newGravityStatus = gravityStatus;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new event gravity status: ");
                newGravityStatus = Console.ReadLine();
                match = Regex.Match(newGravityStatus, pattern);
            }

            return newGravityStatus;
        }

        public static string validateRelatedEntity(string relatedEntity)
        {
            // Regex gender
            string pattern = @"^(Car|Client|Employee)$";
            Match match = Regex.Match(relatedEntity, pattern);
            string newRelatedEntity= relatedEntity;
            while (!match.Success)
            {
                Console.Write("Invalid input! Re-enter a new event related entity: ");
                newRelatedEntity = Console.ReadLine();
                match = Regex.Match(newRelatedEntity, pattern);
            }

            return newRelatedEntity;
        }
    }
}
