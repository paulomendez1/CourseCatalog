import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/enviroments/enviroment';
import { Observable, catchError, map, of, retry, timeout } from 'rxjs';
import { Course } from '../models/Course';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private http: HttpClient) { }

  public apiURL = environment.apiURL + '/course'

  public getAll(): Observable<any> {

    return this.http.get<any>(this.apiURL, { observe: 'response' }).pipe(
      retry(3),
      timeout(15000),
      catchError(error => of([]))
    );
  }

  public getById(id: number): Observable<any> {

    return this.http.get<any>((this.apiURL + `/${id}`), { observe: 'response' }).pipe(
      retry(3),
      timeout(15000),
      catchError(error => of([]))
    );
  }

  public updateCourse(id: number, course: Course): Observable<any> {
    return this.http.put<any>((this.apiURL + `/${id}`), course, { observe: 'response' }).pipe(
      retry(3),
      timeout(15000),
      catchError(error => of([]))
    );
  }
  public createCourse(course: Course) {
    return this.http.post<any>(this.apiURL, course, { observe: 'response' }).pipe(
      retry(3),
      timeout(15000),
      map(data => data.body as boolean),
    );
  }

  public deleteCourse(id: number): Observable<any> {
    return this.http.delete<any>(this.apiURL + `/${id}`).pipe(
      retry(3),
      timeout(15000)
    );
  }
}
