import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeenCommentsPageComponent } from './seen-comments-page.component';

describe('SeenCommentsPageComponent', () => {
  let component: SeenCommentsPageComponent;
  let fixture: ComponentFixture<SeenCommentsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeenCommentsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeenCommentsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
