import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignupAuth } from './signup-auth';

describe('SignupAuth', () => {
  let component: SignupAuth;
  let fixture: ComponentFixture<SignupAuth>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignupAuth]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignupAuth);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
