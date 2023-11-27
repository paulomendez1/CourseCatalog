import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { Course } from 'src/app/models/Course';
import { CourseService } from 'src/app/services/course.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent {

  constructor(private courseService: CourseService, private router: Router) { }

  displayedColumns: string[] = ['courseName', 'roomNumber', 'professorName', 'professorEmail', 'days', 'delete'];
  public dataSource!: any;
  destroy$ = new Subject<void>();
  public isLoading = true;

  ngOnInit(): void {
    this.populateCourses();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public populateCourses() {
    this.courseService.getAll()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          this.isLoading = false;
          this.dataSource = new MatTableDataSource(response.body);
        },
        error: () => {
          this.isLoading = false;
        }
      });
  }

  public deleteItem(id: number) {
    Swal.fire({
      title: 'Do you want to delete this item?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.courseService.deleteCourse(id).subscribe(() => {
          Swal.fire('Item Eliminated!', '', 'success');
          window.location.reload();
        });
      }
    })
  }

  public editItem(id: number) {
    this.router.navigate([`courses/add-course/${id}`]);
  }

  formatSelectedDays(course: Course): string {
    const selectedDaysArray: string[] = [];

    if (course.monday) {
      selectedDaysArray.push('Monday');
    }
    if (course.tuesday) {
      selectedDaysArray.push('Tuesday');
    }
    if (course.wednesday) {
      selectedDaysArray.push('Wednesday');
    }
    if (course.thursday) {
      selectedDaysArray.push('Thursday');
    }
    if (course.friday) {
      selectedDaysArray.push('Friday');
    }

    return selectedDaysArray.length > 0 ? selectedDaysArray.join(', ') : 'No days selected';
  }
}
