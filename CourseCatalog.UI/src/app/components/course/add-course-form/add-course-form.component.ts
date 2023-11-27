import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { Course } from 'src/app/models/Course';
import { CourseService } from 'src/app/services/course.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-course-form',
  templateUrl: './add-course-form.component.html',
  styleUrls: ['./add-course-form.component.css']
})
export class AddCourseFormComponent {
  public courseForm!: FormGroup;
  public course!: Course;
  destroy$ = new Subject<void>();
  public courseId!: number;

  constructor(private formBuilder: FormBuilder, public dialog: MatDialog,
    private courseService: CourseService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.initializeForms();
    this.route.params.subscribe(params => {
      this.courseId = params['courseId']
    });
    this.fillCourseForm();
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public isEditMode(): boolean {
    return this.courseId != undefined
  }

  public fillCourseForm() {
    if (this.courseId) {
      this.courseService.getById(this.courseId).subscribe(response => {
        response = response.body;
        this.courseForm.patchValue({
          courseName: response.courseName,
          roomNumber: response.roomNumber,
          professorName: response.professorName,
          professorEmail: response.professorEmail,
          monday: response.monday,
          thursday: response.thursday,
          wednesday: response.wednesday,
          tuesday: response.tuesday,
          friday: response.friday,
          saturday: response.saturday,
          sunday: response.sunday
        });
      })
    }
  }

  private initializeForms() {
    this.courseForm = this.formBuilder.group({
      courseName: ['', {
        validators: [Validators.required]
      }],
      roomNumber: [0],
      professorName: [''],
      professorEmail: [''],
      monday: [false],
      thursday: [false],
      wednesday: [false],
      tuesday: [false],
      friday: [false],
      saturday: [false],
      sunday: [false]
    }, { validator: this.atLeastOneCheckboxSelected });
  }

  atLeastOneCheckboxSelected(group: FormGroup) {
    const checkboxValues = ['sunday', 'monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];

    const hasAtLeastOneCheckbox = checkboxValues.some(day => group.get(day)?.value);

    return hasAtLeastOneCheckbox ? null : { noCheckboxSelected: true };
  }

  public saveCourse() {
    if (this.courseId) {
      Swal.fire({
        title: 'Do you want to update this course?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, updated it!'
      }).then((result) => {
        if (result.isConfirmed) {
          this.course = { ...this.course, ...this.courseForm.value };
          this.courseService.updateCourse(this.courseId, this.course).subscribe(() => {
            Swal.fire('Course Updated!', '', 'success');
            this.router.navigate([`courses`]);
          });
        }
      })
    }
    else {
      Swal.fire({
        title: 'Do you want to save this course?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, save it!'
      }).then((result) => {
        if (result.isConfirmed) {
          this.course = { ...this.course, ...this.courseForm.value };
          this.courseService.createCourse(this.course).subscribe(() => {
            Swal.fire('Course Created!', '', 'success')
            this.router.navigate([`courses`]);
          });
        }
      })
    }
  }
}
