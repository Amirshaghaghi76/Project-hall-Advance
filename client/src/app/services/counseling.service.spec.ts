import { TestBed } from '@angular/core/testing';

import { CounselingService } from './counseling.service';

describe('CounselingService', () => {
  let service: CounselingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CounselingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
