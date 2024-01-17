import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Comment } from '../../model/comment.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';

@Component({
  standalone:true,
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
  imports:[MatFormFieldModule,MatExpansionModule,MatIconModule,ReactiveFormsModule,FormsModule],
})
export class CommentComponent {
  commentRes: Comment | undefined;
  constructor(private fb: FormBuilder, private http: HttpClient) { }
  commentFg = this.fb.group({
    nameCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(8)]],
    phoneNumberCtrl: ['', [Validators.minLength(11), Validators.maxLength(11)]],
    opinionCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(60)]]
  });

  get NameCtrl(): FormControl {
    return this.commentFg.get('nameCtrl') as FormControl
  }
  get PhoneNumberCtrl(): FormControl {
    return this.commentFg.get('phoneNumberCtrl') as FormControl
  }
  get OpinionCtrl(): FormControl {
    return this.commentFg.get('opinionCtrl') as FormControl
  }

  registerComment(): void {
    console.log(this.commentFg.value);
    let comment: Comment = {
      name: this.NameCtrl.value,
      phoneNumber: this.PhoneNumberCtrl.value,
      opinion: this.OpinionCtrl.value
    }

    this.http.post<Comment>('http://localhost:5000/api/coment/register', comment).subscribe(
      {
        next: res => {
          this.commentRes = res;
          console.log(res);
        }
      }
    )
  }

  ClearForm(): void {
    this.commentFg.reset();
  }
}
