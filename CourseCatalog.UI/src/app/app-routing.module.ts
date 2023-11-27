import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CourseComponent } from './components/course/course.component';
import { AddCourseFormComponent } from './components/course/add-course-form/add-course-form.component';

const routes: Routes = [
  { path: 'courses', component: CourseComponent },
  { path: 'courses/add-course', component: AddCourseFormComponent },
  { path: 'courses/add-course/:courseId', component: AddCourseFormComponent },
  { path: '', pathMatch: 'full', redirectTo: 'courses' },
  { path: '**', redirectTo: 'courses' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
