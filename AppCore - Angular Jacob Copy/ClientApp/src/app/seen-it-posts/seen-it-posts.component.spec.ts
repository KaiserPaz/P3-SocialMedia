import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeenItPostsComponent } from './seen-it-posts.component';

describe('SeenItPostsComponent', () => {
  let component: SeenItPostsComponent;
  let fixture: ComponentFixture<SeenItPostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeenItPostsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeenItPostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
