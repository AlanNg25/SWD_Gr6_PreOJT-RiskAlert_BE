using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCodeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "major",
                columns: table => new
                {
                    MajorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MajorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_major", x => x.MajorID);
                });

            migrationBuilder.CreateTable(
                name: "semester",
                columns: table => new
                {
                    SemesterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_semester", x => x.SemesterID);
                });

            migrationBuilder.CreateTable(
                name: "subject",
                columns: table => new
                {
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_user_major_MajorID",
                        column: x => x.MajorID,
                        principalTable: "major",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "curriculum",
                columns: table => new
                {
                    CurriculumID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermNo = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MajorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculum", x => x.CurriculumID);
                    table.ForeignKey(
                        name: "FK_curriculum_major_MajorID",
                        column: x => x.MajorID,
                        principalTable: "major",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_curriculum_subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "subject",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "syllabus",
                columns: table => new
                {
                    SyllabusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SyllabusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrerequisitesSubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentTasks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScoringCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditHours = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_syllabus", x => x.SyllabusID);
                    table.ForeignKey(
                        name: "FK_syllabus_subject_PrerequisitesSubjectID",
                        column: x => x.PrerequisitesSubjectID,
                        principalTable: "subject",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_syllabus_subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "subject",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_course_semester_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "semester",
                        principalColumn: "SemesterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_course_subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "subject",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_course_user_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "user",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    NotificationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_notification_user_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "user",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "prediction",
                columns: table => new
                {
                    PredictionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Probability = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prediction", x => x.PredictionID);
                    table.ForeignKey(
                        name: "FK_prediction_user_StudentID",
                        column: x => x.StudentID,
                        principalTable: "user",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "enrollment",
                columns: table => new
                {
                    EnrollmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MajorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentStatus = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollment", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_enrollment_course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_enrollment_major_MajorID",
                        column: x => x.MajorID,
                        principalTable: "major",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_enrollment_user_StudentID",
                        column: x => x.StudentID,
                        principalTable: "user",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "grade",
                columns: table => new
                {
                    GradeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoreAverage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grade", x => x.GradeID);
                    table.ForeignKey(
                        name: "FK_grade_course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grade_user_StudentID",
                        column: x => x.StudentID,
                        principalTable: "user",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "attendance",
                columns: table => new
                {
                    AttendanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendNumber = table.Column<int>(type: "int", nullable: false),
                    SessionNumber = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_attendance_enrollment_EnrollmentID",
                        column: x => x.EnrollmentID,
                        principalTable: "enrollment",
                        principalColumn: "EnrollmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "riskAnalysis",
                columns: table => new
                {
                    RiskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiskLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_riskAnalysis", x => x.RiskID);
                    table.ForeignKey(
                        name: "FK_riskAnalysis_enrollment_EnrollmentID",
                        column: x => x.EnrollmentID,
                        principalTable: "enrollment",
                        principalColumn: "EnrollmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gradeDetail",
                columns: table => new
                {
                    GradeDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ScoreWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinScr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gradeDetail", x => x.GradeDetailID);
                    table.ForeignKey(
                        name: "FK_gradeDetail_grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "grade",
                        principalColumn: "GradeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suggestion",
                columns: table => new
                {
                    SuggestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdvisorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suggestion", x => x.SuggestionID);
                    table.ForeignKey(
                        name: "FK_suggestion_riskAnalysis_RiskID",
                        column: x => x.RiskID,
                        principalTable: "riskAnalysis",
                        principalColumn: "RiskID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_suggestion_user_AdvisorID",
                        column: x => x.AdvisorID,
                        principalTable: "user",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendance_EnrollmentID",
                table: "attendance",
                column: "EnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_course_SemesterID",
                table: "course",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_course_SubjectID",
                table: "course",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_course_TeacherID",
                table: "course",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_curriculum_MajorID",
                table: "curriculum",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_curriculum_SubjectID",
                table: "curriculum",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollment_CourseID",
                table: "enrollment",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollment_MajorID",
                table: "enrollment",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollment_StudentID",
                table: "enrollment",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_grade_CourseID",
                table: "grade",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_grade_StudentID",
                table: "grade",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_gradeDetail_GradeID",
                table: "gradeDetail",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_notification_ReceiverID",
                table: "notification",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_prediction_StudentID",
                table: "prediction",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_riskAnalysis_EnrollmentID",
                table: "riskAnalysis",
                column: "EnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_suggestion_AdvisorID",
                table: "suggestion",
                column: "AdvisorID");

            migrationBuilder.CreateIndex(
                name: "IX_suggestion_RiskID",
                table: "suggestion",
                column: "RiskID");

            migrationBuilder.CreateIndex(
                name: "IX_syllabus_PrerequisitesSubjectID",
                table: "syllabus",
                column: "PrerequisitesSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_syllabus_SubjectID",
                table: "syllabus",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_user_MajorID",
                table: "user",
                column: "MajorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendance");

            migrationBuilder.DropTable(
                name: "curriculum");

            migrationBuilder.DropTable(
                name: "gradeDetail");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "prediction");

            migrationBuilder.DropTable(
                name: "suggestion");

            migrationBuilder.DropTable(
                name: "syllabus");

            migrationBuilder.DropTable(
                name: "grade");

            migrationBuilder.DropTable(
                name: "riskAnalysis");

            migrationBuilder.DropTable(
                name: "enrollment");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "semester");

            migrationBuilder.DropTable(
                name: "subject");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "major");
        }
    }
}
