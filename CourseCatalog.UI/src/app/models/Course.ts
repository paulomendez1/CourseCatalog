export class Course {
    public constructor() {
        this.courseId = 0;
        this.courseName = '';
        this.isDeleted = false;
        this.monday = false;
        this.tuesday = false;
        this.wednesday = false;
        this.thursday = false;
        this.friday = false;
        this.saturday = false;
        this.sunday = false;
    }

    courseId: number;
    courseName: string;
    roomNumber?: number;
    isDeleted: boolean;
    monday: boolean;
    tuesday: boolean;
    wednesday: boolean;
    thursday: boolean;
    friday: boolean;
    saturday: boolean;
    sunday: boolean;
    professorName?: string;
    professorEmail?: string;
}
