using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work.Data.Initializers
{
    public class Constants
    {
        /// <summary>
        /// List of all the countries in the world
        /// https://gist.github.com/901679
        /// http://en.wikipedia.org/wiki/Country_dial_codes#Complete_listing
        /// </summary>
        public static string[] COUNTRIES = {
            "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra",
            "Angola", "Anguilla", "Antarctica", "Antigua And Barbuda", "Argentina",
            "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan",
            "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus",
            "Belgium", "Belize", "Benin", "Bermuda", "Bhutan",
            "Bolivia", "Bosnia Hercegovina", "Botswana", "Bouvet Island", "Brazil",
            "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Byelorussian SSR",
            "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands",
            "Central African Republic", "Chad", "Chile", "China", "Christmas Island",
            "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Cook Islands",
            "Costa Rica", "Cote D'Ivoire", "Croatia", "Cuba", "Cyprus",
            "Czech Republic", "Czechoslovakia", "Denmark", "Djibouti", "Dominica",
            "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador",
            "England", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia",
            "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France",
            "Gabon", "Gambia", "Georgia", "Germany", "Ghana",
            "Gibraltar", "Great Britain", "Greece", "Greenland", "Grenada",
            "Guadeloupe", "Guam", "Guatemela", "Guernsey", "Guiana",
            "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard Islands",
            "Honduras", "Hong Kong", "Hungary", "Iceland", "India",
            "Indonesia", "Iran", "Iraq", "Ireland", "Isle Of Man",
            "Israel", "Italy", "Jamaica", "Japan", "Jersey",
            "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, South",
            "Korea, North", "Kuwait", "Kyrgyzstan", "Lao People's Dem. Rep.", "Latvia",
            "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein",
            "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar",
            "Malawi", "Malaysia", "Maldives", "Mali", "Malta",
            "Mariana Islands", "Marshall Islands", "Martinique", "Mauritania", "Mauritius",
            "Mayotte", "Mexico", "Micronesia", "Moldova", "Monaco",
            "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar",
            "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles",
            "Neutral Zone", "New Caledonia", "New Zealand", "Nicaragua", "Niger",
            "Nigeria", "Niue", "Norfolk Island", "Northern Ireland", "Norway",
            "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea",
            "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland",
            "Polynesia", "Portugal", "Puerto Rico", "Qatar", "Reunion",
            "Romania", "Russian Federation", "Rwanda", "Saint Helena", "Saint Kitts",
            "Saint Lucia", "Saint Pierre", "Saint Vincent", "Samoa", "San Marino",
            "Sao Tome and Principe", "Saudi Arabia", "Scotland", "Senegal", "Seychelles",
            "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
            "Somalia", "South Africa", "South Georgia", "Spain", "Sri Lanka",
            "Sudan", "Suriname", "Svalbard", "Swaziland", "Sweden",
            "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikista", "Tanzania",
            "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago",
            "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu",
            "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States",
            "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City State", "Venezuela",
            "Vietnam", "Virgin Islands", "Wales", "Western Sahara", "Yemen",
            "Yugoslavia", "Zaire", "Zambia", "Zimbabwe" };

        public static string[] OCCUPATIONS = {
            "Academic", "Accountant", "Actor", "Architect", "Artist",
            "Business Manager", "Carpenter", "Chief Executive",
            "Cinematographer", "Civil Servant", "Coach", "Composer",
            "Computer programmer", "Cook", "Counsellor", "Doctor",
            "Driver", "Economist", "Editor", "Electrician", "Engineer",
            "Executive Producer", "Fixer", "Graphic Designer", "Hairdresser",
            "Headhunter", "HR - Recruitment", "Information Officer",
            "IT Consultant", "Journalist", "Lawyer / Solicitor", "Lecturer",
            "Librarian", "Mechanic", "Model", "Musician", "Office Worker",
            "Performer", "Photographer", "Presenter", "Producer / Director",
            "Project Manager", "Researcher", "Salesman", "Social Worker",
            "Soldier", "Sportsperson", "Student", "Teacher", "Technical Crew",
            "Technical Writer", "Therapist", "Translator", "Waitress / Waiter",
            "Web designer / author", "Writer", "Other"};

        public static string[] MALE_NAMES = { "James", "John", "Robert", "Michael", "William", "David", "Richard", "Charles", "Joseph", "Thomas" };
        public static string[] FEMALE_NAMES = { "Sophia", "Emily", "Chloe", "Olivia", "Tiffany", "Fiona", "Jessica", "Vivian", "Isabella", "Nicole" };
        public static string[] LAST_NAMES = { "Lee", "Smith", "Lam", "Martin", "Brown", "Roy", "Tremblay", "McGraw", "Gagnon", "Wilson", "Clark", "Johnson", "White", "Williams", "Côté", "Taylor", "Campbell", "Anderson", "Chan", "Jones" };
        public static string[] POSITIONS = { "Software Developer", "Tester", "Support Technician" };
        public static string[] TEAM_NAMES = { "Development", "Testing", "Support" };
        public static string[] COMPANY_TYPES = { "Inc", "Corp", "Ltd", "LLC", "" };
        public static string[] STREET_TYPES = { "St", "Way", "Ave", "Cres", "Rd" };
    }
}
