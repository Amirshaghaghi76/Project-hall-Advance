import { TestBed } from '@angular/core/testing';

import { AddHallService } from './add-hall.service';

describe('AddHallService', () => {
  let service: AddHallService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddHallService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
